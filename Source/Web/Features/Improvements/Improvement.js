/*---------------------------------------------------------------------------------------------
 *  This file is an automatically generated ReadModel Proxy
 *  
 *--------------------------------------------------------------------------------------------*/
import { ReadModel } from  '@dolittle/readmodels';

export class Improvement extends ReadModel
{
    constructor() {
        super();
        this.artifact = {
           id: '7936fa2b-4af0-486e-a6e3-9ac5b3ca7c15',
           generation: '1'
        };
        this.id = '00000000-0000-0000-0000-000000000000';
        this.improvable = '00000000-0000-0000-0000-000000000000';
        this.pullRequest = false;
        this.completed = new Date();
        this.failed = new Date();
        this.version = '';
    }
}