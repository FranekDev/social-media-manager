// export type ValidationErrorResponse = {
//     // type: string;
//     // title: string;
//     // status: number;
//     // errors: Record<string, string[]>;
//     // traceId: string;
//     errors: ValidationError[];
// };

export type ValidationErrorResponse = {
    reasons: string[];
    message: string;
    metadata: any;
};