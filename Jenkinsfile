// define port for services
def selectPort(serviceName) {
    switch (serviceName) {
        case 'frontend':
            return '3000'
            break
        case 'gateway':
            return '7000'
            break
        case 'user':
            return '7135'
            break
        case 'catalog':
            return '7021'
            break
        default:
            echo 'Select port error!!'
            break
    }
}

pipeline {
    agent any
    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose.yml'
        DOCKER_HUB_USERNAME = 'tomcorleone'
        TAG_NAME_IMAGE_FRONTEND = 'elly-frontend'
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
                    // Lấy danh sách file thay đổi
                    sh "git log --oneline -2"  // Xem 2 commit gần nhất
                    sh "git status"  // Kiểm tra trạng thái workspace

                    sh "git reset --hard"
                    sh "git clean -fd" 

                    def changedFiles = sh(
                        script: "git diff --name-only lp/241118_jenkins_test",
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
        stage('Build') {
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
                        def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
                        
                        // Build Docker image
                        sh """
                        docker-compose -f ${DOCKER_COMPOSE_FILE} build ${service}
                        docker tag ${TAG_NAME_IMAGE_FRONTEND} ${dockerImageTag}
                        """

                         // Push Docker image lên Docker Hub
                        echo "Push image: ${TAG_NAME_IMAGE_FRONTEND}"
                        try {
                            sh "docker push ${dockerImageTag}"
                        } catch (e) {
                            error "Push to dockerhub failed: ${e}"
                        }

                        //clean image
                        // echo "Clear image: ${service}"
                        // sh "docker image rm...."

                        // Deploy (ví dụ: chỉ start container thay đổi)
                        // sh """
                        // docker-compose -f ${DOCKER_COMPOSE_FILE} up -d ${service}
                        // """                        
                    }
                }
            }
        }
        stage('Deploy server'){
            steps{
               script{
                sshagent(['ellly_ssh_remote']) {
                    sh 'ssh -o StrictHostKeyChecking=no -l phantanloc 14.225.254.168'

                    env.CHANGED_SERVICES.split(' ').each { service ->
                        echo "Building and Deploying ${service}"
                        if (service != "frontend"){
                            echo "skip service ${service}"
                            return
                        }
                        // Tạo tag với ngày giờ
                        def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
                        def imageName = "elly_${service}"
                        def port = selectPort(service)
                        echo "Start pull and run image"
                        echo "dockerImageTag: ${dockerImageTag}. imageName: ${imageName}. port: ${dockerImageTag}"

                    sh  """
                        # Kéo Docker image từ Docker Hub
                        docker pull ${dockerImageTag}

                        # Dừng và xóa container cũ nếu có
                        docker stop ${imageName} || true
                        docker rm ${imageName} || true

                        # Chạy container mới
                        docker run -d --name ${imageName} -p ${port}:80 ${dockerImageTag}
                        """                
                    }                   
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