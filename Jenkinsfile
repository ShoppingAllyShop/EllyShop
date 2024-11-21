pipeline {
    agent any
    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose.yml'
    }
    stages {
        stage('Checkout clone or update repo') {
            steps {
                script {
                    git branch: 'lp/241118_jenkins_test', url: 'https://github.com/ShoppingAllyShop/EllyShop.git'
                }
            }
        }
        stage('Detect Changed Services') {
            steps {
                script {
                    // Sử dụng git diff để tìm các thư mục service thay đổi
                    echo "Start Detect Changed Services aaaaaaaaaaa"
                    def aaa = sh(script: "git diff --name-only HEAD~1", returnStdout: true).trim()
                    echo "met qua ${aaa}"

                    def changes = sh(script: "git diff --name-only HEAD~1 | grep '^service'", returnStdout: true).trim()
                    echo "changes aaaaaaaaaa: ${changes}"

                    if (changes) {
                        def services = changes.split('\n')
                                                .collect { it.split('/')[0] }
                                                .unique()
                        echo "services: ${services}"

                        env.CHANGED_SERVICES = services.join(' ')
                    } else {
                        env.CHANGED_SERVICES = ''
                    }
                }
            }
        }
        stage('Build and Deploy') {
            when {
                expression { env.CHANGED_SERVICES != '' }
            }
            steps {
                script {
                    // Lặp qua các service thay đổi và thực hiện build + deploy
                    env.CHANGED_SERVICES.split(' ').each { service ->
                        echo "Building and Deploying ${service}"

                        // Build Docker image
                        sh """
                        docker-compose -f ${DOCKER_COMPOSE_FILE} build ${service}
                        """

                        // Deploy (ví dụ: chỉ start container thay đổi)
                        sh """
                        docker-compose -f ${DOCKER_COMPOSE_FILE} up -d ${service}
                        """
                    }
                }
            }
        }
    }
    post {
        always {
            echo "Pipeline completed."
        }
    }
}