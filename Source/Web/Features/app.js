import { PLATFORM } from 'aurelia-pal';
import style from '../styles/style.scss';
import { inject } from 'aurelia-dependency-injection';

export class app {
    constructor() {
    }

    configureRouter(config, router) {
        config.options.pushState = true;
        config.map([
            { route: ['', 'Improvables'], name: 'Improvables', moduleId: PLATFORM.moduleName('Improvables/List') },
            { route: 'Improvements/:improvable', name: 'Improvements', moduleId: PLATFORM.moduleName('Improvements/Index') },

            { route: ['GitHub/', 'GitHub/Repositories', 'GitHub/Repositories/:status/:id?'], name: 'GitHub - Repositories', moduleId: PLATFORM.moduleName('SourceControl/GitHub/Repositories') },
            { route: 'GitHub/Authorize', name: 'GitHub - Authorize', moduleId: PLATFORM.moduleName('SourceControl/GitHub/Authorize') }
        ]);

        this.router = router;
    }
}
