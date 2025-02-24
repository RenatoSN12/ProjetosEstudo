import styles from './Footer.module.css'
import { FaLinkedin, FaInstagram } from "react-icons/fa";

function Footer(){
    return (
        <footer className={styles.footer}>
        <ul className={styles.social_list}>
            <li><FaLinkedin/></li>
            <li><FaInstagram/></li>
        </ul>

        <p className={styles.copy_right}>
            Notion do Erick &copy; 2025
        </p>
        </footer>
    )
}

export default Footer