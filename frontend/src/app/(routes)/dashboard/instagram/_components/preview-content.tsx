import Image from "next/image";
import React from "react";
import { InstagramFormType } from "@/app/(routes)/dashboard/instagram/_hooks/use-instagram-schedule-post-form";

type PreviewContentProps = {
    type: InstagramFormType;
    fileUrl: string;
    caption: string;
}

export default function PreviewContent({ fileUrl, caption, type }: PreviewContentProps) {
    return (
        <div className="w-fit h-full flex flex-col space-y-2">
            <h2>Podgląd</h2>
            {type === "post" ? (
                <Image src={fileUrl}
                       alt="Podgląd zdjęcia"
                       width={250}
                       height={500}
                       className="rounded-sm"
                />
            ) : (
                <video
                    src={fileUrl}
                    width={250}
                    height={500}
                    className="rounded-md"
                    controls
                />
            )}
            <p className="max-w-60 text-wrap break-words">{caption}</p>
        </div>
    );
}