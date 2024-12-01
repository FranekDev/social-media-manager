import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";

export const getInstagramCommentReplies = async (accessToken: string, commentId: string): Promise<ApiResponse<InstagramComment[]>> => {
    const response = await api.call<InstagramComment[]>(API_PATHS.instagram.getCommentReplies(commentId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}