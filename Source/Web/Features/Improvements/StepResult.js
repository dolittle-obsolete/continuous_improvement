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
           id: 'f76339d2-6407-41e4-b728-70e9ac7a6271',
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