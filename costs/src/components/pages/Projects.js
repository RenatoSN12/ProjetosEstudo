import {useLocation} from 'react-router-dom'
import LinkButton from '../layout/LinkButton'
import Container from '../layout/Container'
import styles from '../pages/Projects.module.css'
import {useState, useEffect} from 'react'
import ProjectCard from '../project/ProjectCard'
import Message from "../layout/Message"


function Projects(){

    const [projects, setProjects] = useState([])

    const location = useLocation()
    let message = ''
    if (location.state) {
        message = location.state.message
    }

    useEffect(() => {
        fetch('http://localhost:5000/projects', {
            method: 'GET',
            headers: {
                'Content-Type':'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => {
            setProjects(data)
        })
        .catch((err) => console.log(err))
    }, [])

    return(
        <div className={styles.project_container}>
            <div className={styles.title_container}>
                <h1>Meus Projetos</h1>
                <LinkButton to="/newproject" text="Criar Projeto"/>
            </div>
            {message && <Message type="sucess" msg={message}/>}
            <Container customClass='start'>
                {projects.length > 0 && (
                    projects.map((project) => (
                        <ProjectCard 
                            id={project.id}
                            name={project.name} 
                            category={project.category.name}
                            budget={project.budget}
                            key={project.id}
                        />
                    ))
                )}
            </Container>
        </div>
    )
}

export default Projects