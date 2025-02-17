import React, { useState } from "react";
import { Drawer, List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import EventIcon from "@mui/icons-material/Event";
import AddCircleIcon from "@mui/icons-material/AddCircle";
import NotificationsIcon from "@mui/icons-material/Notifications";
import NewAppointmentModal from "./NewAppointmentModal";

export default function Sidebar({ open, toggleDrawer }) {
  const [modalOpen, setModalOpen] = useState(false);

  const handleOpenModal = () => setModalOpen(true);
  const handleCloseModal = () => setModalOpen(false);

  const menuItems = [
    { text: "Início", icon: <HomeIcon /> },
    { text: "Agenda", icon: <EventIcon /> },
    { text: "Novo Compromisso", icon: <AddCircleIcon />, action: handleOpenModal },
    { text: "Notificações", icon: <NotificationsIcon /> },
  ];

  return (
    <>
      <Drawer anchor="left" open={open} onClose={toggleDrawer(false)} 
        sx={{ "& .MuiDrawer-paper": { width: 250, backgroundColor: "#1a1a1a" } }}
      >
        <List>
          {menuItems.map((item, index) => (
            <ListItem 
              button 
              key={index} 
              sx={{ padding: "15px 20px", color: "white", "&:hover": { backgroundColor: "#333"} }}
              onClick={item.action ? item.action : null}
            > 
              <ListItemIcon sx={{ color: "white" }}>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItem>
          ))}
        </List>
      </Drawer>

      <NewAppointmentModal open={modalOpen} handleClose={handleCloseModal} />
    </>
  );
}
