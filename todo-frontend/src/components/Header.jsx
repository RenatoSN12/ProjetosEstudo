import React, { useState } from "react";
import { AppBar, Toolbar, IconButton, Typography } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Sidebar from "./Sidebar";

export default function Header() {
  const [open, setOpen] = useState(false);

  const toggleDrawer = (state) => () => {
    setOpen(state);
  };

  return (
    <>
      {/* Header */}
      <AppBar position="fixed">
        <Toolbar>
          <IconButton edge="start" color="inherit" aria-label="menu" onClick={toggleDrawer(true)}>
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            Meu App
          </Typography>
        </Toolbar>
      </AppBar>

      {/* Sidebar (Drawer separado) */}
      <Sidebar open={open} toggleDrawer={toggleDrawer} />
    </>
  );
}
