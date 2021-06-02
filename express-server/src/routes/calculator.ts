import StatusCodes from 'http-status-codes';
import { Request, Response } from 'express';

const { BAD_REQUEST, UNPROCESSABLE_ENTITY, OK } = StatusCodes;

interface BadParams {
    status: number;
    message: string;
    success: false;
}

interface GoodParams {
    first: number;
    second: number;
    success: true;
}

type Params = GoodParams | BadParams;

function getQueryParams(req: Request) : Params {
    const first = parseInt(req.params.first, 10);
    const second = parseInt(req.params.second, 10);
    if (isNaN(first)) {
        return {
            success: false,
            status: BAD_REQUEST,
            message: `The value ${req.params.first} is not a valid number`
        }
    }
    if (isNaN(second)) {
        return {
            success: false,
            status: BAD_REQUEST,
            message: `The value ${req.params.second} is not a valid number`
        }
    }
    return {
        first,
        second, 
        success: true
    }
}

type Op = (first:number, second: number) => number;

function getHandler(method: Op) {
    return (req: Request, res: Response) =>  {
        const params = getQueryParams(req);
        if (!params.success) {
            const {status, message} = params;
            res.status(status).send({message});
            return;
        }

        const {first, second} = params;
        try {
            const result = method(first, second);
            res.status(OK).send({first, second, result});
        } catch(error) {
            const {message} = error;
            res.status(UNPROCESSABLE_ENTITY).send({message});
        }

    }
}

export const add = getHandler((first, second) => first + second);
export const subtract = getHandler((first, second) => first - second);
export const multiply = getHandler((first, second) => first * second);
export const divide = getHandler((first, second) => {
    if (second === 0) {
        throw Error("Divisor cannot be zero");
    }
    return first / second;
});
