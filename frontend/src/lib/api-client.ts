import { ApiEndpoint } from "@/configurations/api-paths";
import { ValidationErrorResponse } from "@/types/api/error";

type RequestOptions = {
    body?: any;
    headers?: Record<string, string>;
    params?: Record<string, string | number | boolean | undefined | null>;
};

export const api = {
    async call<T>(endpoint: ApiEndpoint, options?: RequestOptions): Promise<T> {

        const url = buildUrl(endpoint, options?.params || {});

        const response = await fetch(url, {
            method: endpoint.method,
            headers: {
                "Content-Type": "application/json",
                ...options?.headers,
            },
            body: JSON.stringify(options?.body),
        });

        if (!response.ok) {
            // throw new Error(`Failed to fetch ${endpoint.url}`);
            return response.json();
        }

        return response.json();
    }
};

const buildUrl = (endpoint: ApiEndpoint, params: Record<string, string | number | boolean | undefined | null>): string => {
    const url = new URL(endpoint.url);

    Object.keys(params).forEach((key) => {
        const value = params[key];

        if (value !== undefined && value !== null) {
            url.searchParams.append(key, String(value));
        }
    });

    return url.toString();
}