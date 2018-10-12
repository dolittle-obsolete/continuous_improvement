import { customElement, containerless, bindable } from 'aurelia-framework';

@customElement('side-bar')
@containerless()
export class side_bar {
    @bindable expanded = false;
    @bindable hide_trigger;
    @bindable is_subnavigation;
    constructor() {
    }
    
    toggle_side_bar() {
        this.expanded = !this.expanded;
    }
}
