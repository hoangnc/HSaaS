export class Stack {
    constructor(capability) {
        this.data = [];
        this.capability = capability;
    }

    isEmpty() {
        return this.data.length === 0;
    }

    isFull() {
        return this.data.length === this.capability;
    }

    push(item) {
        if (this.isFull()) return false;

        this.data.push(item);
        return true;
    }

    pop() {
        if (this.isEmpty()) return undefined;
        return this.data.pop();
    }

    peek() {
        if (this.isEmpty()) return undefined;
        return this.data[this.data.length - 1];
    }

    clear() {
        this.data.length = 0;
    }
}