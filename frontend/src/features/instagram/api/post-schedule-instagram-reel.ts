import { InstagramScheduleReelRequest } from "@/types/instagram/request/instagram-schedule-reel";
import { api } from "@/lib/api-client";
import { InstagramSchedulePostResponse } from "@/types/instagram/response/instagram-api-response";
import { API_PATHS } from "@/configurations/api-paths";
import { ApiResponse } from "@/types/api/api-response";

export const scheduleInstagramReel = async (accessToken: string, body: InstagramScheduleReelRequest): Promise<ApiResponse<InstagramSchedulePostResponse>> => {
    const response = await api.call<InstagramSchedulePostResponse>(API_PATHS.instagram.scheduleReel, {
        body,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response;
};