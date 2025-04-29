import styles from './Footer.module.css'
import { FaLinkedin, FaInstagram } from "react-icons/fa";

function Footer(){
    return (
        <footer className={styles.footer}>
        <ul className={styles.social_list}>
            <li><a href='https://www.linkedin.com/in/renato-nascimento-82b334210/' target='_blank' rel='noopener noreferrer'><FaLinkedin/></a></li>
            <li><a href='https://www.instagram.com/prosperefinance/' target='_blank' rel='noopener noreferrer'><FaInstagram/></a></li>
        </ul>

        <p className={styles.copy_right}>
            Protótipo Notion &copy; 2025
        </p>
        </footer>
    )
}

export default Footer