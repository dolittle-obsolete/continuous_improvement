/*---------------------------------------------------------------------------------------------
 *  This file is an automatically generated ReadModel Proxy
 *  
 *--------------------------------------------------------------------------------------------*/
import { ReadModel } from  '@dolittle/readmodels';

export class StepResult extends ReadModel
{
    constructor() {
        super();
        this.artifact = {
           id: '715cc94c-313e-40a7-9b7b-05c336f81985',
           generation: '1'
        };
        this.severity = {};
        this.project = '';
        this.file = '';
        this.line = 0;
        this.column = 0;
        this.code = '';
        this.message = '';
        this.originalLine = 0;
    }
}