const BASE_URL = "https://localhost:7114/api";

type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';

export type ApiEndpoint = {
    url: string;
    method: HttpMethod;
};

type UserEndpoints = {
    login: ApiEndpoint;
    register: ApiEndpoint;
};

type InstagramEndpoints = {
    schedulePost: ApiEndpoint;
    getPosts: ApiEndpoint;
    unpublished: ApiEndpoint;
    scheduleReel: ApiEndpoint;
    insights: ApiEndpoint;
    getUser: ApiEndpoint;
};

type ApiStructure = {
    user: UserEndpoints;
    instagram: InstagramEndpoints;
}

export const API_PATHS: ApiStructure = {
    user: {
        login: {
            url: `${BASE_URL}/Account/login`,
            method: 'POST',
        },
        register: {
            url: `${BASE_URL}/Account/register`,
            method: 'POST',
        },
    },
    instagram: {
        schedulePost: {
            url: `${BASE_URL}/InstagramMedia`,
            method: 'POST',
        },
        getPosts: {
            url: `${BASE_URL}/InstagramMedia`,
            method: 'GET',
        },
        unpublished: {
            url: `${BASE_URL}/InstagramMedia/unpublished`,
            method: 'GET',
        },
        scheduleReel: {
            url: `${BASE_URL}/InstagramMedia/reel`,
            method: 'POST',
        },
        insights: {
            url: `${BASE_URL}/InstagramMedia/insights`,
            method: 'POST',
        },
        getUser: {
            url: `${BASE_URL}/InstagramUser`,
            method: 'GET',
        },
    }
} as const;
