import { ApiResponse } from "@/types/api/api-response";
import { FacebookPagePostComment } from "@/types/facebook/facebook-page-post-comment";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";

export const getFacebookPagePostComments = async (accessToken: string, pageId: string, postId: string): Promise<ApiResponse<FacebookPagePostComment[]>> => {
    return await api.call<FacebookPagePostComment[]>(API_PATHS.facebook.getPagePostComments(pageId, postId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });
};