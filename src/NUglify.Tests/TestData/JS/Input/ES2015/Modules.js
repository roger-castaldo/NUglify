﻿// http://es6-features.org/#ValueExportImport
//  lib/math.js
export function sum(x, y) { return x + y };
export const pi = 3.141593;

//  someApp.js
import * as math from "lib/math";
console.log("2π = " + math.sum(math.pi, math.pi));

//  otherApp.js
import { sum, pi } from "lib/math";
console.log("2π = " + sum(pi, pi));

// http://es6-features.org/#DefaultWildcard
//  lib/mathplusplus.js
export * from "lib/math";
export const e = 2.71828182846;
export default (x) => Math.exp(x);

//  someApp.js
import exp, { pi, e } from "lib/mathplusplus";
console.log("e^{π} = " + exp(pi));