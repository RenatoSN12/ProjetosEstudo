import { useState } from "react";
import { IoIosMenu } from "react-icons/io";
import logo from "../../img/logo.png";
import Navbar from "./Navbar";
import styles from "./Header.module.css";

function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <header className={styles.header}>
      <div className={styles.menuIcon} onClick={toggleMenu}>
        <IoIosMenu size={30} />
      </div>

      <div className={`${styles.logo} ${isMenuOpen ? styles.logoHidden : ""}`}>
        <img src={logo} alt="logo-notion"/>
        <h1>Notion do Erick</h1>
      </div>

      <Navbar isMenuOpen={isMenuOpen} />
    </header>
  );
}

export default Header;