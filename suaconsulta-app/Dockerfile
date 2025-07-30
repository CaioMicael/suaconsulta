FROM node:16

WORKDIR /suaconsulta-app

# Copy package.json
COPY package.json ./

# Install dependencies
RUN npm install

# Copy the entire project directory
COPY . ./

# Build the application
RUN npm run build

# Install serve
RUN npm install -g serve

# Expose port 3000
EXPOSE 3000

# Command with SPA support
CMD ["serve", "-s", "build", "-l", "3000"]

#docker run -d -p 3000:3000 --name minha-react-app minha-react-app