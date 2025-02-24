import styles from './Advantage.module.css';

function Advantage({ title, text, image, inverse }) {
    return (
        <section className={styles.Advantage}>
            {inverse ? (
                <>
                    <img 
                        src={image} 
                        alt="ilustrative-image" 
                        className={inverse ? styles.inverseImage : ''}
                    />
                    <div className={styles.Info}>
                        <h2>{title}</h2>
                        <p>{text}</p>
                    </div>
                </>
            ) : (
                <>
                    <div className={styles.Info}>
                        <h2>{title}</h2>
                        <p>{text}</p>
                    </div>
                    <img 
                        src={image} 
                        alt="ilustrative-image" 
                        className={inverse ? styles.inverseImage : ''}    
                    />
                </>
            )}
        </section>
    );
}

export default Advantage;