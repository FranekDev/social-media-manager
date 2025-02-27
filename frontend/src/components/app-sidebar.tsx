﻿"use client";

import { Calendar, Facebook, Home, Instagram } from "lucide-react";
import {
    Sidebar,
    SidebarContent, SidebarFooter,
    SidebarGroup,
    SidebarGroupContent,
    SidebarGroupLabel,
    SidebarMenu,
    SidebarMenuButton,
    SidebarMenuItem,
} from "@/components/ui/sidebar";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu";
import SignOutButton from "@/components/auth/signout-button";
import UserInfo from "@/components/auth/user-info";
import TikTokIcon from "@/components/icons/tiktok-icon";
import { ModeToggle } from "@/components/ModeToggle";
import Link from "next/link";
import { usePathname } from "next/navigation";

const items = [
    {
        title: "Start",
        url: "/",
        icon: Home,
    },
    {
        title: "Publikacje",
        url: "/scheduled",
        icon: Calendar,
    }
];

type Platform = {
    name: string;
    url: string;
    icon: JSX.Element;
};

const platforms: Platform[] = [
    {
        name: "Instagram",
        url: "/dashboard/instagram",
        icon: <Instagram />,
    },
    {
        name: "Facebook",
        url: "/dashboard/facebook",
        icon: <Facebook />,
    },
    {
        name: "TikTok",
        url: "/dashboard/tiktok",
        icon: <TikTokIcon />,
    }
];

export function AppSidebar() {

    const currentPath = usePathname();

    if (currentPath.includes("login") || currentPath.includes("register")) {
        return null;
    }

    return (
        <Sidebar>
            <SidebarContent>
                <SidebarGroup>
                    <SidebarGroupLabel>Social Media Manager</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {items.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild>
                                        <Link href={item.url} className={currentPath == item.url ? "bg-zinc-800" : ""}>
                                            <item.icon/>
                                            <span>{item.title}</span>
                                        </Link>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
                <SidebarGroup>
                    <SidebarGroupLabel>Platformy</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {platforms.map((platform) => (
                                <SidebarMenuItem key={platform.name}>
                                    <SidebarMenuButton asChild>
                                        <Link id={platform.name} href={platform.url} className={currentPath == platform.url ? "bg-zinc-800" : ""}>
                                            {platform.icon}
                                            <span>{platform.name}</span>
                                        </Link>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
            </SidebarContent>
            <SidebarFooter>
                <SidebarMenu>
                    <SidebarMenuItem>
                        <ModeToggle/>
                    </SidebarMenuItem>
                    <SidebarMenuItem>
                        <DropdownMenu>
                            <DropdownMenuTrigger asChild>
                                <SidebarMenuButton>
                                    <UserInfo />
                                </SidebarMenuButton>
                            </DropdownMenuTrigger>
                            <DropdownMenuContent
                                side="top"
                                className="w-[--radix-popper-anchor-width]"
                            >
                                <DropdownMenuItem>
                                    <span>Account</span>
                                </DropdownMenuItem>
                                <DropdownMenuItem>
                                    <SignOutButton/>
                                </DropdownMenuItem>
                            </DropdownMenuContent>
                        </DropdownMenu>
                    </SidebarMenuItem>
                </SidebarMenu>
            </SidebarFooter>
        </Sidebar>
    )
}
