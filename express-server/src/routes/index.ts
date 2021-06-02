import { Router } from 'express';
import { add, subtract, multiply, divide } from './calculator';


// User-route
const calcRouter = Router();
calcRouter.get('/add/:first/:second', add);
calcRouter.get('/subtract/:first/:second', subtract);
calcRouter.get('/multiply/:first/:second', multiply);
calcRouter.get('/divide/:first/:second', divide);


// Export the base-router
const baseRouter = Router();
baseRouter.use('/calculator', calcRouter);
export default baseRouter;
