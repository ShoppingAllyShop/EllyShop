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
                    // Lấy danh sách file thay đổi (ví dụ giả định ở đây)
                    def changedFiles = sh(
                        script: "git diff --name-only HEAD~1",
                        returnStdout: true
                    ).trim()

                    echo "Changed files: ${changedFiles}"

                    // Tách rootpath
                    if (changedFiles) {
                        def rootPaths = changedFiles
                            .split('\n')                          // Chia từng dòng
                            .collect { it.split('/')[0].toLowerCase() }         // Lấy phần rootpath (trước `/source`)
                            .unique()                            // Loại bỏ trùng lặp

                        echo "Unique root paths: ${rootPaths}"  

                        // Gán vào biến môi trường nếu cần dùng tiếp
                        env.CHANGED_SERVICES = rootPaths.join(' ')
                    } else {
                        echo "No changes detected."
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
                        if (service != "frontend"){
                            echo "skip service ${service}"
                        }
                        // Build Docker image
                        sh """
                        docker-compose -f ${DOCKER_COMPOSE_FILE} build ${   }
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