import compare from 'just-compare';

/* tslint:disable:no-non-null-assertion */
class ListNode {
    constructor(value) {
        this.value = value;
    }
}
class LinkedList {
    constructor() {
        this.size = 0;
    }
    get head() {
        return this.first;
    }
    get tail() {
        return this.last;
    }
    get length() {
        return this.size;
    }
    attach(value, previousNode, nextNode) {
        if (!previousNode)
            return this.addHead(value);
        if (!nextNode)
            return this.addTail(value);
        const node = new ListNode(value);
        node.previous = previousNode;
        previousNode.next = node;
        node.next = nextNode;
        nextNode.previous = node;
        this.size++;
        return node;
    }
    attachMany(values, previousNode, nextNode) {
        if (!values.length)
            return [];
        if (!previousNode)
            return this.addManyHead(values);
        if (!nextNode)
            return this.addManyTail(values);
        const list = new LinkedList();
        list.addManyTail(values);
        list.first.previous = previousNode;
        previousNode.next = list.first;
        list.last.next = nextNode;
        nextNode.previous = list.last;
        this.size += values.length;
        return list.toNodeArray();
    }
    detach(node) {
        if (!node.previous)
            return this.dropHead();
        if (!node.next)
            return this.dropTail();
        node.previous.next = node.next;
        node.next.previous = node.previous;
        this.size--;
        return node;
    }
    add(value) {
        return {
            after: (...params) => this.addAfter.call(this, value, ...params),
            before: (...params) => this.addBefore.call(this, value, ...params),
            byIndex: (position) => this.addByIndex(value, position),
            head: () => this.addHead(value),
            tail: () => this.addTail(value),
        };
    }
    addMany(values) {
        return {
            after: (...params) => this.addManyAfter.call(this, values, ...params),
            before: (...params) => this.addManyBefore.call(this, values, ...params),
            byIndex: (position) => this.addManyByIndex(values, position),
            head: () => this.addManyHead(values),
            tail: () => this.addManyTail(values),
        };
    }
    addAfter(value, previousValue, compareFn = compare) {
        const previous = this.find(node => compareFn(node.value, previousValue));
        return previous ? this.attach(value, previous, previous.next) : this.addTail(value);
    }
    addBefore(value, nextValue, compareFn = compare) {
        const next = this.find(node => compareFn(node.value, nextValue));
        return next ? this.attach(value, next.previous, next) : this.addHead(value);
    }
    addByIndex(value, position) {
        if (position < 0)
            position += this.size;
        else if (position >= this.size)
            return this.addTail(value);
        if (position <= 0)
            return this.addHead(value);
        const next = this.get(position);
        return this.attach(value, next.previous, next);
    }
    addHead(value) {
        const node = new ListNode(value);
        node.next = this.first;
        if (this.first)
            this.first.previous = node;
        else
            this.last = node;
        this.first = node;
        this.size++;
        return node;
    }
    addTail(value) {
        const node = new ListNode(value);
        if (this.first) {
            node.previous = this.last;
            this.last.next = node;
            this.last = node;
        }
        else {
            this.first = node;
            this.last = node;
        }
        this.size++;
        return node;
    }
    addManyAfter(values, previousValue, compareFn = compare) {
        const previous = this.find(node => compareFn(node.value, previousValue));
        return previous ? this.attachMany(values, previous, previous.next) : this.addManyTail(values);
    }
    addManyBefore(values, nextValue, compareFn = compare) {
        const next = this.find(node => compareFn(node.value, nextValue));
        return next ? this.attachMany(values, next.previous, next) : this.addManyHead(values);
    }
    addManyByIndex(values, position) {
        if (position < 0)
            position += this.size;
        if (position <= 0)
            return this.addManyHead(values);
        if (position >= this.size)
            return this.addManyTail(values);
        const next = this.get(position);
        return this.attachMany(values, next.previous, next);
    }
    addManyHead(values) {
        return values.reduceRight((nodes, value) => {
            nodes.unshift(this.addHead(value));
            return nodes;
        }, []);
    }
    addManyTail(values) {
        return values.map(value => this.addTail(value));
    }
    drop() {
        return {
            byIndex: (position) => this.dropByIndex(position),
            byValue: (...params) => this.dropByValue.apply(this, params),
            byValueAll: (...params) => this.dropByValueAll.apply(this, params),
            head: () => this.dropHead(),
            tail: () => this.dropTail(),
        };
    }
    dropMany(count) {
        return {
            byIndex: (position) => this.dropManyByIndex(count, position),
            head: () => this.dropManyHead(count),
            tail: () => this.dropManyTail(count),
        };
    }
    dropByIndex(position) {
        if (position < 0)
            position += this.size;
        const current = this.get(position);
        return current ? this.detach(current) : undefined;
    }
    dropByValue(value, compareFn = compare) {
        const position = this.findIndex(node => compareFn(node.value, value));
        return position < 0 ? undefined : this.dropByIndex(position);
    }
    dropByValueAll(value, compareFn = compare) {
        const dropped = [];
        for (let current = this.first, position = 0; current; position++, current = current.next) {
            if (compareFn(current.value, value)) {
                dropped.push(this.dropByIndex(position - dropped.length));
            }
        }
        return dropped;
    }
    dropHead() {
        const head = this.first;
        if (head) {
            this.first = head.next;
            if (this.first)
                this.first.previous = undefined;
            else
                this.last = undefined;
            this.size--;
            return head;
        }
        return undefined;
    }
    dropTail() {
        const tail = this.last;
        if (tail) {
            this.last = tail.previous;
            if (this.last)
                this.last.next = undefined;
            else
                this.first = undefined;
            this.size--;
            return tail;
        }
        return undefined;
    }
    dropManyByIndex(count, position) {
        if (count <= 0)
            return [];
        if (position < 0)
            position = Math.max(position + this.size, 0);
        else if (position >= this.size)
            return [];
        count = Math.min(count, this.size - position);
        const dropped = [];
        while (count--) {
            const current = this.get(position);
            dropped.push(this.detach(current));
        }
        return dropped;
    }
    dropManyHead(count) {
        if (count <= 0)
            return [];
        count = Math.min(count, this.size);
        const dropped = [];
        while (count--)
            dropped.unshift(this.dropHead());
        return dropped;
    }
    dropManyTail(count) {
        if (count <= 0)
            return [];
        count = Math.min(count, this.size);
        const dropped = [];
        while (count--)
            dropped.push(this.dropTail());
        return dropped;
    }
    find(predicate) {
        for (let current = this.first, position = 0; current; position++, current = current.next) {
            if (predicate(current, position, this))
                return current;
        }
        return undefined;
    }
    findIndex(predicate) {
        for (let current = this.first, position = 0; current; position++, current = current.next) {
            if (predicate(current, position, this))
                return position;
        }
        return -1;
    }
    forEach(iteratorFn) {
        for (let node = this.first, position = 0; node; position++, node = node.next) {
            iteratorFn(node, position, this);
        }
    }
    get(position) {
        return this.find((_, index) => position === index);
    }
    indexOf(value, compareFn = compare) {
        return this.findIndex(node => compareFn(node.value, value));
    }
    toArray() {
        const array = new Array(this.size);
        this.forEach((node, index) => (array[index] = node.value));
        return array;
    }
    toNodeArray() {
        const array = new Array(this.size);
        this.forEach((node, index) => (array[index] = node));
        return array;
    }
    toString(mapperFn = JSON.stringify) {
        return this.toArray()
            .map(value => mapperFn(value))
            .join(' <-> ');
    }
    // Cannot use Generator type because of ng-packagr
    *[Symbol.iterator]() {
        for (let node = this.first, position = 0; node; position++, node = node.next) {
            yield node.value;
        }
    }
}

/*
 * Public API Surface of utils
 */

/**
 * Generated bundle index. Do not edit.
 */

export { LinkedList, ListNode };
//# sourceMappingURL=abp-utils.js.map
