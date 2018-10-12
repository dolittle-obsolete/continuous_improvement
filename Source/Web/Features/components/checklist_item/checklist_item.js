import { customElement, containerless, bindable } from 'aurelia-framework';

@customElement('checklist-item')
@containerless()
export class checklist_item {
    @bindable
    nav_url;
    @bindable
    icon_name;
    @bindable
    text;
    status_success_icon = require(`../../../assets/icons/check_badge.svg`);
    status_fail_icon = require(`../../../assets/icons/fail_badge.svg`);
    show_success() {
        let d = new Date();
        return d.getTime() % 2 == 0;
    }

    constructor() {}

    get icon_url() {
        let icon_file = '';
        if (this.icon_name) {
            icon_file = require(`../../../assets/icons/${this.icon_name}.svg`);
        }
        return icon_file;
    }
}
