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
    getComments: (mediaId: string) => ApiEndpoint;
    getCommentReplies: (commentId: string) => ApiEndpoint;
    postCommentReply: (commentId: string) => ApiEndpoint;
    getScheduledPosts: ApiEndpoint;
    getScheduledReels: ApiEndpoint;
};

type FacebookEndpoints = {
    getUserPages: ApiEndpoint;
    pageData: (pageId: string) => ApiEndpoint;
    getPublishedPosts: (pageId: string) => ApiEndpoint;
    getPagePostComments: (pageId: string, postId: string) => ApiEndpoint;
    schedulePost: ApiEndpoint;
    getScheduledPosts: ApiEndpoint;
    createPagePostComment: ApiEndpoint;
};

type TikTokEndpoints = {
    getUser: ApiEndpoint;
    getPublishedPhotos: ApiEndpoint;
    getPublishedVideos: ApiEndpoint;
    getScheduledPhotos: ApiEndpoint;
    getSchduledVideos: ApiEndpoint;
    schedulePhoto: ApiEndpoint;
    scheduleVideo: ApiEndpoint;
    getPostsStats: ApiEndpoint;
};

type ApiStructure = {
    user: UserEndpoints;
    instagram: InstagramEndpoints;
    facebook: FacebookEndpoints;
    tiktok: TikTokEndpoints;
}

export const API_PATHS: ApiStructure = {
    instagram: {
        schedulePost: {
            url: `${BASE_URL}/InstagramMedia`,
            method: 'POST',
        },
        getPosts: {
            url: `${BASE_URL}/InstagramMedia`,
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
        unpublished: {
            url: `${BASE_URL}/InstagramMedia/unpublished`,
            method: 'GET',
        },

        getUser: {
            url: `${BASE_URL}/InstagramUser`,
            method: 'GET',
        },
        getComments: (mediaId: string): ApiEndpoint => ({
            url: `${BASE_URL}/InstagramComment/${mediaId}/comments`,
            method: "GET"
        }),
        getCommentReplies: (commentId: string): ApiEndpoint => ({
            url: `${BASE_URL}/InstagramComment/${commentId}/replies`,
            method: "GET"
        }),
        postCommentReply: (commentId: string): ApiEndpoint => ({
            url: `${BASE_URL}/InstagramComment/${commentId}/replies`,
            method: "POST"
        }),
        getScheduledPosts: {
            url: `${BASE_URL}/InstagramMedia/unpublished`,
            method: "GET"
        },
        getScheduledReels: {
            url: `${BASE_URL}/InstagramMedia/unpublished/reels`,
            method: "GET"
        }
    },
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
    facebook: {
        getUserPages: {
            url: `${BASE_URL}/FacebookUser/pages`,
            method: "GET"
        },
        pageData: (pageId: string): ApiEndpoint => ({
            url: `${BASE_URL}/FacebookUser/${pageId}`,
            method: "GET"
        }),
        getPublishedPosts: (pageId: string): ApiEndpoint => ({
            url: `${BASE_URL}/FacebookUser/${pageId}/published-posts`,
            method: "GET"
        }),
        getPagePostComments: (pageId: string, postId: string): ApiEndpoint => ({
            url: `${BASE_URL}/FacebookUser/${pageId}/${postId}/comments`,
            method: "POST"
        }),
        schedulePost: {
            url: `${BASE_URL}/FacebookUser/post`,
            method: "POST"
        },
        getScheduledPosts: {
            url: `${BASE_URL}/FacebookUser/scheduled`,
            method: "GET"
        },
        createPagePostComment: {
            url: `${BASE_URL}/FacebookUser/comment`,
            method: "POST"
        },
    },
    tiktok: {
        getUser: {
            url: `${BASE_URL}/TikTokUser/info`,
            method: "GET"
        },
        getPublishedPhotos: {
            url: `${BASE_URL}/TikTokPost/photos/published`,
            method: "GET"
        },
        getPublishedVideos: {
            url: `${BASE_URL}/TikTokPost/videos/published`,
            method: "GET"
        },
        getScheduledPhotos: {
            url: `${BASE_URL}/TikTokPost/photos/scheduled`,
            method: "GET"
        },
        getSchduledVideos: {
            url: `${BASE_URL}/TikTokPost/videos/scheduled`,
            method: "GET"
        },
        schedulePhoto: {
            url: `${BASE_URL}/TikTokPost/photos`,
            method: "POST"
        },
        scheduleVideo: {
            url: `${BASE_URL}/TikTokPost/videos`,
            method: "POST"
        },
        getPostsStats: {
            url: `${BASE_URL}/TikTokPost/videos/stats`,
            method: "GET"
        },
    }
} as const;
