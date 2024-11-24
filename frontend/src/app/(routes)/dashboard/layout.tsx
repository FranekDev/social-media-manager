import React from "react";

export default async function DashboardLayout({
 children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <div className="p-12 w-full h-full">
            {children}
        </div>
    );
}
