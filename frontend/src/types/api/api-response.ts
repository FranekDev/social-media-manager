import { ValidationErrorResponse } from "@/types/api/error";

export interface ApiResponse<T> {
    data: T | null;
    errors: ValidationErrorResponse[];
}