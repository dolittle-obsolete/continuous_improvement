/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { QueryCoordinator } from '@dolittle/queries';
import { inject } from 'aurelia-dependency-injection';
import { StepResultsForStep } from './StepResultsForStep';
import { RawLogForStep } from './RawLogForStep'

@inject(QueryCoordinator)
export class ImprovementDetails {
    #queryCoordinator;
    results = [];
    rawLog = "";

    rawView = false;

    #stepResultsForStepPopulated = false;
    #rawLogForStepPopulated = false;

    #project;
    #stepNumber;
    #version;

    constructor(queryCoordinator) {
        this.#queryCoordinator = queryCoordinator;
    }

    async activate(params) {
        this.#project = params.id;
        this.#stepNumber = params.step;
        this.#version = params.version;

        await this.populateStepResults();
    }

    async populateStepResults() {
        let query = new StepResultsForStep();
        query.project = this.#project;
        query.version = this.#version;
        query.number = this.#stepNumber;

        let result = await this.#queryCoordinator.execute(query);
        this.results = result.items;
        
        this.#stepResultsForStepPopulated = true;
    }

    async populateRawLog() {
        let query = new RawLogForStep();
        query.project = this.#project;
        query.version = this.#version;
        query.number = this.#stepNumber;

        let result = await this.#queryCoordinator.execute(query);
        this.rawLog = result.items[0].content;
        this.rawLog = this.rawLog.replace(/\n/g, '<br />');

        this.#rawLogForStepPopulated = true;
    }

    setFocusedView() {
        if( !this.#stepResultsForStepPopulated ) {
            this.populateStepResults();
        }
        this.rawView = false;
    }

    setRawView() {
        if( !this.#rawLogForStepPopulated ) {
            this.populateRawLog();
        }
        this.rawView = true;
    }

    async refresh() {
        await this.populateStepResults();
        await this.populateRawLog();
    }
}