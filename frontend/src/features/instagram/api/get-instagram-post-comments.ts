import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";

export const getInstagramPostComments = async (accessToken: string, mediaId: string): Promise<ApiResponse<InstagramComment[]>> => {
    const response = await api.call<InstagramComment[]>(API_PATHS.instagram.getComments(mediaId), {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}