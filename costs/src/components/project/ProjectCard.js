import styles from './ProjectCard.module.css'
import { Link } from 'react-router-dom'
import {BsPencil, BsFillTrashFill, BsPen} from 'react-icons/bs'

function ProjectCard({id, name, budget, category, handleRemove})
{

    const removeProject = (e) => {
        e.preventDefault()
        handleRemove(id)
    }

    return (
        <div className={styles.project_card}>
            <h4>{name}</h4>
            <p>
                <span>Orçamento:</span> {budget}
            </p>
            <p className={styles.category_text}>
            <span className = {`${styles[category?.toLowerCase() || '']}`}> </span> {category}
            </p>
            <div className={styles.project_card_actions}>
                <Link to={`/project/${id}`}><BsPencil/>Editar</Link>
                <button onClick={removeProject}>
                    <BsFillTrashFill/> Excluir
                </button>
            </div>
        </div>
    )
}

export default ProjectCard