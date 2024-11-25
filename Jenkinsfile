pipeline {
    agent any
    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose.yml'
        DOCKER_HUB_USERNAME = 'tomcorleone'
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
        stage('Login to Docker') {
            steps {
                withCredentials([string(credentialsId: 'elly_dockerhub_token', variable: 'DOCKER_HUB_TOKEN')]) {
                    sh '''
                    echo $DOCKER_HUB_TOKEN | docker login -u $DOCKER_HUB_USERNAME --password-stdin
                    '''
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
                     echo "CHANGED_SERVICES: ${env.CHANGED_SERVICES}"
                    env.CHANGED_SERVICES.split(' ').each { service ->
                        echo "Building and Deploying ${service}"
                        if (service != "frontend"){
                            echo "skip service ${service}"
                            return
                        }
                        // Tạo tag với ngày giờ
                        def imageTag = "tomcorleone/elly-mayo-frontend:${new Date().format('yyyyMMdd-HHmmss')}"
                        echo "imageTag: ${imageTag}"

                        // Build Docker image
                        echo "Build service: ${service}"
                        sh """
                        docker-compose -f ${DOCKER_COMPOSE_FILE} build ${service}
                        docker tag ${service} ${imageTag}
                        """

                         // Push Docker image lên Docker Hub
                        echo "Push image: ${service}"
                        try {
                            sh "docker push ${imageTag}"
                        } catch (e) {
                            error "Push to dockerhub failed: ${e}"
                        }

                        // Deploy (ví dụ: chỉ start container thay đổi)
                        // sh """
                        // docker-compose -f ${DOCKER_COMPOSE_FILE} up -d ${service}
                        // """

                        
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