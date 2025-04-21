FROM node:16

WORKDIR /suaconsulta-app

# Copy package.json
COPY package.json ./

# Install dependencies
RUN npm install

# Copy the entire project directory
# This ensures all files including public/index.html are copied
COPY . ./

# Debug to verify files were copied correctly
RUN ls -la public/

# Build the application
RUN npm run build

# Install serve for production hosting
RUN npm install -g serve

# Expose port 3000
EXPOSE 3000

# Command to run the application
CMD ["serve", "-s", "build"]