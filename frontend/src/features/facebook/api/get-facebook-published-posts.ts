import { ApiResponse } from "@/types/api/api-response";
import { FacebookPublishedPost } from "@/types/facebook/facebook-published-post";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";

export const getFacebookPublishedPosts = async (accessToken: string, pageId: string): Promise<ApiResponse<FacebookPublishedPost[]>> => {
    return await api.call<FacebookPublishedPost[]>(API_PATHS.facebook.getPublishedPosts(pageId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    })
};