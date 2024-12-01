import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { InstagramMediaDetail } from "@/types/instagram/response/instagram-media-detail";
import { ApiResponse } from "@/types/api/api-response";

export const getInstagramUserMedia = async (accessToken: string, pageNumber: number = 1, pageSize: number = 10): Promise<ApiResponse<InstagramMediaDetail[]>> => {
    const response = await api.call<InstagramMediaDetail[]>(API_PATHS.instagram.getPosts, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        },
        params: {
            pageNumber,
            pageSize
        }
    });

    return response;
}