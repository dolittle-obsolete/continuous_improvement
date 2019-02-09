/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

 const warning = require('./warning.png');
 const error = require('./error.png');


export class StepResultSeverityIconValueConverter {
    toView(value) {
        switch(value) {
            case 1: return warning; 
            case 2: return error;
            case 3: return "info.png";
        }
    }
}