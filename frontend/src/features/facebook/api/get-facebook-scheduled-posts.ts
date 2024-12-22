import { ApiResponse } from "@/types/api/api-response";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { FacebookFeedPost } from "@/types/facebook/facebook-feed-post";

export const getFacebookScheduledPosts = async (accessToken: string): Promise<ApiResponse<FacebookFeedPost[]>> => {
    const response = await api.call<FacebookFeedPost[]>(API_PATHS.facebook.getScheduledPosts, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
};