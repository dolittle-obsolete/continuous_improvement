/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

export class StepTitleValueConverter {
    toView(value) {
        switch (value.type) {
            case "7b9af50d-b146-4bd2-9792-cd4a5efdd8af": return "Get latest source";
            case "77c7d69f-2698-4f42-bb33-2409ead97786": return "Version";
            case "efe78367-8670-4ebb-8f9a-be92d5fc05e5": return "Build Core";
            case "f7a18897-f625-4c26-be38-68c11e0e8d33": return "Build Interaction";
        }
    }
}