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

const items = [
    {
        title: "Home",
        url: "/",
        icon: Home,
    },
    {
        title: "Publikacje",
        url: "#",
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
        url: "#",
        icon: <Facebook />,
    },
    {
        name: "TikTok",
        url: "#",
        icon: <TikTokIcon />,
    }
];

export function AppSidebar() {
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
                                        <a href={item.url}>
                                            <item.icon/>
                                            <span>{item.title}</span>
                                        </a>
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
                                        <a href={platform.url}>
                                            {platform.icon}
                                            <span>{platform.name}</span>
                                        </a>
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
