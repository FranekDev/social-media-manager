import React from "react";
import { PhotoFormValues, VideoFormValues } from "@/app/(routes)/dashboard/tiktok/_hooks/use-tiktok-schedule-post-form";
import Image from "next/image";

type PreviewContentProps = {
    type: "photo" | "video";
    data: PhotoFormValues | VideoFormValues;
};

export default function PreviewContent({ type, data }: PreviewContentProps) {
    if (type === "photo") {
        const photoData = data as PhotoFormValues;
        return (
            <div className="max-w-60">
                {photoData.images && (
                    <Image
                        src={URL.createObjectURL(photoData.images)}
                        alt="Podgląd zdjęcia"
                        width={250}
                        height={500}
                        className="rounded-sm"
                    />
                )}
                <p className="text-xl">{photoData.title}</p>
                <p>{photoData.description}</p>
            </div>
        );
    } else {
        const videoData = data as VideoFormValues;
        return (
            <div className="max-w-60">
                {videoData.video && (
                    <video
                        src={URL.createObjectURL(videoData.video)}
                        width={250}
                        height={500}
                        className="rounded-md"
                        controls
                    />
                )}
                <p className="text-xl">{videoData.title}</p>
            </div>
        );
    }
}
