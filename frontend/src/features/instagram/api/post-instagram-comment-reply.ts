import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";

export const postInstagramCommentReply = async (accessToken: string, message: string, commentId: string): Promise<ApiResponse<InstagramCommentReply>> => {
    const response = await api.call<InstagramCommentReply>(API_PATHS.instagram.postCommentReply(commentId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        },
        body: { message }
    });

    return response;
}