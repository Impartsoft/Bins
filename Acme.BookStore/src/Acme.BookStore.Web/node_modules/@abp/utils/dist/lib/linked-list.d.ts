export declare class ListNode<T = any> {
    readonly value: T;
    next: ListNode | undefined;
    previous: ListNode | undefined;
    constructor(value: T);
}
export declare class LinkedList<T = any> {
    private first;
    private last;
    private size;
    get head(): ListNode<T> | undefined;
    get tail(): ListNode<T> | undefined;
    get length(): number;
    private attach;
    private attachMany;
    private detach;
    add(value: T): {
        after: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>;
        before: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>;
        byIndex: (position: number) => ListNode<T>;
        head: () => ListNode<T>;
        tail: () => ListNode<T>;
    };
    addMany(values: T[]): {
        after: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>[];
        before: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>[];
        byIndex: (position: number) => ListNode<T>[];
        head: () => ListNode<T>[];
        tail: () => ListNode<T>[];
    };
    addAfter(value: T, previousValue: T): ListNode<T>;
    addAfter(value: T, previousValue: any, compareFn: ListComparisonFn<T>): ListNode<T>;
    addBefore(value: T, nextValue: T): ListNode<T>;
    addBefore(value: T, nextValue: any, compareFn: ListComparisonFn<T>): ListNode<T>;
    addByIndex(value: T, position: number): ListNode<T>;
    addHead(value: T): ListNode<T>;
    addTail(value: T): ListNode<T>;
    addManyAfter(values: T[], previousValue: T): ListNode<T>[];
    addManyAfter(values: T[], previousValue: any, compareFn: ListComparisonFn<T>): ListNode<T>[];
    addManyBefore(values: T[], nextValue: T): ListNode<T>[];
    addManyBefore(values: T[], nextValue: any, compareFn: ListComparisonFn<T>): ListNode<T>[];
    addManyByIndex(values: T[], position: number): ListNode<T>[];
    addManyHead(values: T[]): ListNode<T>[];
    addManyTail(values: T[]): ListNode<T>[];
    drop(): {
        byIndex: (position: number) => ListNode<T>;
        byValue: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>;
        byValueAll: (...params: [T] | [any, ListComparisonFn<T>]) => ListNode<T>[];
        head: () => ListNode<T>;
        tail: () => ListNode<T>;
    };
    dropMany(count: number): {
        byIndex: (position: number) => ListNode<T>[];
        head: () => ListNode<T>[];
        tail: () => ListNode<T>[];
    };
    dropByIndex(position: number): ListNode<T> | undefined;
    dropByValue(value: T): ListNode<T> | undefined;
    dropByValue(value: any, compareFn: ListComparisonFn<T>): ListNode<T> | undefined;
    dropByValueAll(value: T): ListNode<T>[];
    dropByValueAll(value: any, compareFn: ListComparisonFn<T>): ListNode<T>[];
    dropHead(): ListNode<T> | undefined;
    dropTail(): ListNode<T> | undefined;
    dropManyByIndex(count: number, position: number): ListNode<T>[];
    dropManyHead(count: Exclude<number, 0>): ListNode<T>[];
    dropManyTail(count: Exclude<number, 0>): ListNode<T>[];
    find(predicate: ListIteratorFn<T>): ListNode<T> | undefined;
    findIndex(predicate: ListIteratorFn<T>): number;
    forEach<R = boolean>(iteratorFn: ListIteratorFn<T, R>): void;
    get(position: number): ListNode<T> | undefined;
    indexOf(value: T): number;
    indexOf(value: any, compareFn: ListComparisonFn<T>): number;
    toArray(): T[];
    toNodeArray(): ListNode<T>[];
    toString(mapperFn?: ListMapperFn<T>): string;
    [Symbol.iterator](): any;
}
export declare type ListMapperFn<T = any> = (value: T) => any;
export declare type ListComparisonFn<T = any> = (value1: T, value2: any) => boolean;
export declare type ListIteratorFn<T = any, R = boolean> = (node: ListNode<T>, index?: number, list?: LinkedList) => R;
