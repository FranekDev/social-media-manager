import { ApiResponse } from "@/types/api/api-response";
import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { FacebookNewPagepostComment } from "@/types/facebook/request/facebook-new-pagepost-comment";

export const createPagepostComment = async (accessToken: string, body: FacebookNewPagepostComment): Promise<ApiResponse<FacebookNewPagepostComment>> => {
    const response = await api.call<FacebookNewPagepostComment>(API_PATHS.facebook.createPagePostComment, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        },
        body
    });

    return response;
};