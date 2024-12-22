import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";
import { InstagramPost } from "@/types/instagram/response/instagram-post";

export const getInstagramScheduledPosts = async (accessToken: string): Promise<ApiResponse<InstagramPost[]>> => {
    const response = await api.call<InstagramPost[]>(API_PATHS.instagram.getScheduledPosts, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}