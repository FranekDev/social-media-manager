import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import { FacebookSchedulePostRequest } from "@/types/facebook/request/facebook-schedule-post-request";
import { FacebookSchedulePostResponse } from "@/types/facebook/response/facebook-schedule-post-response";
import { ApiResponse } from "@/types/api/api-response";

export const scheduleFacebookPost = async (accessToken: string, body: FacebookSchedulePostRequest): Promise<ApiResponse<FacebookSchedulePostResponse>> => {
    const response = await api.call<FacebookSchedulePostResponse>(API_PATHS.facebook.schedulePost, {
        body,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
}