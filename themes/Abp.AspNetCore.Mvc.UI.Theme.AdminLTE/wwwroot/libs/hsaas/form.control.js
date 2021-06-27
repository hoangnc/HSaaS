import ControlType from './controltype';

export class FormControl {
    constructor(control: {}, controlType: ControlType.Input, initialValue: {}) {
        this.control = control;
        this.controlType = controlType;
        this.oldValue = initialValue;
        this.newValue = initialValue;
    }
}