import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { InstagramMediaInsightRequest } from "@/types/instagram/request/instagram-media-insight";
import { InstagramInsightsResponse } from "@/types/instagram/response/instagram media insight";
import { ApiResponse } from "@/types/api/api-response";

export const getInstagramMediaInsight = async (accessToken: string, request: InstagramMediaInsightRequest): Promise<ApiResponse<InstagramInsightsResponse>> => {
    const response = await api.call<InstagramInsightsResponse>(API_PATHS.instagram.insights, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        },
        body: request
    });

    console.log(response);

    return response;
};