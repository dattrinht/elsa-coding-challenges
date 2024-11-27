# Real-Time Vocabulary Quiz Coding Challenge
*(The submission is below.)*

## Overview

Welcome to the Real-Time Quiz coding challenge! Your task is to create a technical solution for a real-time quiz feature for an English learning application. This feature will allow users to answer questions in real-time, compete with others, and see their scores updated live on a leaderboard.

## Acceptance Criteria

1. **User Participation**:
   - Users should be able to join a quiz session using a unique quiz ID.
   - The system should support multiple users joining the same quiz session simultaneously.

2. **Real-Time Score Updates**:
   - As users submit answers, their scores should be updated in real-time.
   - The scoring system must be accurate and consistent.

3. **Real-Time Leaderboard**:
   - A leaderboard should display the current standings of all participants.
   - The leaderboard should update promptly as scores change.

## Challenge Requirements

### Part 1: System Design

1. **System Design Document**:
   - **Architecture Diagram**: Create an architecture diagram illustrating how different components of the system interact. This should include all components required for the feature, including the server, client applications, database, and any external services.
   - **Component Description**: Describe each component's role in the system.
   - **Data Flow**: Explain how data flows through the system from when a user joins a quiz to when the leaderboard is updated.
   - **Technologies and Tools**: List and justify the technologies and tools chosen for each component.

### Part 2: Implementation

1. **Pick a Component**:
   - Implement one of the core components below using the technologies that you are comfortable with. The rest of the system can be mocked using mock services or data.

2. **Requirements for the Implemented Component**:
   - **Real-time Quiz Participation**: Users should be able to join a quiz session using a unique quiz ID.
   - **Real-time Score Updates**: Users' scores should be updated in real-time as they submit answers.
   - **Real-time Leaderboard**: A leaderboard should display the current standings of all participants in real-time.

3. **Build For the Future**:
   - **Scalability**: Design and implement your component with scalability in mind. Consider how the system would handle a large number of users or quiz sessions. Discuss any trade-offs you made in your design and implementation.
   - **Performance**: Your component should perform well even under heavy load. Consider how you can optimize your code and your use of resources to ensure high performance.
   - **Reliability**: Your component should be reliable and handle errors gracefully. Consider how you can make your component resilient to failures.
   - **Maintainability**: Your code should be clean, well-organized, and easy to maintain. Consider how you can make it easy for other developers to understand and modify your code.
   - **Monitoring and Observability**: Discuss how you would monitor the performance of your component and diagnose issues. Consider how you can make your component observable.

## Submission Guidelines

Candidates are required to submit the following as part of the coding challenge:

1. **System Design Documents**:
   - **Architecture Diagram**: Illustrate the interaction of system components (server, client applications, database, etc.).
   - **Component Descriptions**: Explain the role of each component.
   - **Data Flow**: Describe how data flows from user participation to leaderboard updates.
   - **Technology Justification**: List the chosen technologies and justify why they were selected.

2. **Working Code**:
   - Choose one of the core components mentioned in the requirements and implement it using your preferred technologies. The rest of the system can be mocked using appropriate mock services or data.
   - Ensure the code meets criteria such as scalability, performance, reliability, maintainability, and observability.

3. **Video Submission**:
   - Record a short video (5-10 minutes) where you address the following:
     - **Introduction**: Introduce yourself and state your name.
     - **Assignment Overview**: Describe the technical assignment that ELSA gave in your own words. Feel free to mention any assumptions or clarifications you made.
     - **Solution Overview**: Provide a crisp overview of your solution, highlighting key design and implementation elements.
     - **Demo**: Run the code on your local machine and walk us through the output or any tests you’ve written to verify the functionality.
     - **Conclusion**: Conclude with any remarks, such as challenges faced, learnings, or further improvements you would make.

   **Video Requirements**:
   - The video must be between **5-10 minutes**. Any submission beyond 10 minutes will be rejected upfront.
   - Use any recording device (smartphone, webcam, etc.), ensuring good audio and video quality.
   - Ensure clear and concise communication.
---
[excalidraw board](https://excalidraw.com/#json=T28cb4oNEVQdD5Esblv3V,HSBxrqQ6WhhSxjmHYGoGyw)
## Context diagram
![image](https://github.com/user-attachments/assets/f1553c4b-0132-4c58-b216-a67e98a172f6)

The context diagram illustrates the interactions between different components and users within the system:

- **User**: Joins a quiz session with a unique ID and receives real-time update data.
- **Quiz Session Component**: Handles the quiz session by fetching the quiz from the Quiz Manager Component, updating scores, and calculating leaderboard rankings.
- **Quiz Manager Component**: Managed by the admin, responsible for managing the quiz database and providing quizzes for sessions.
- **Notification Component**: Updates scores and sends real-time update data back to the user.
- **Leaderboard Component**: Updates the leaderboard based on the scores and rankings calculated by the Quiz Session Component.

## Container diagram
![image](https://github.com/user-attachments/assets/1e57b8ec-d461-4dad-a037-beb73b1bda76)

This container diagram outlines the architecture and interactions between various components of a real-time quiz session system. Below are the key elements and their technical choices:

- **User Interaction**:
   - **User**: Joins a quiz session with a unique ID via the Quiz Session UI.
   - **Admin**: Manages the quiz database via the Quiz Manager UI.
- **User Interface**:
   - **Quiz Session UI**: Built with NextJS for its capabilities in server-side rendering and client-side navigation. It allows users to join quiz sessions and make API calls using JSON/HTTPS.
   - **Quiz Manager UI**: Also built with NextJS, enabling admins to manage the quiz database and make API calls using JSON/HTTPS.
- **Server**:
   - **Quiz Session Server**: Developed with .NET, it handles API calls from the Quiz Session UI and makes synchronous calls using gRPC. It reads/writes data using SQL/TCP to the Quiz Session Database.
   - **Quiz Manager Server**: Built with .NET, it handles API calls from the Quiz Manager UI and reads/writes data using NoSQL/TCP to the Quiz Manager Database.
- **Databases**
   - **Quiz Session Database**: Uses Postgres for storing quiz session data, selected for its robustness and support for complex relationships.
   - **Quiz Manager Database**: Utilizes MongoDB for storing quiz management data, chosen for its flexibility and scalability in handling JSON-like documents. The quiz entity can be easily modeled within the document. It does not require a transactional database.
- **Real-Time Communication**:
   - **SignalR**: SignalR is chosen for its ease of integration with .NET and efficient real-time communication.
- **Message Broker**:
   - **Kafka**: Utilizes Kafka to subscribe to and publish messages, decoupling components and handling concurrency problems by processing messages with ordering guarantees (instead of locking the resource).
- **Distributed Cache**:
   - **Redis**: In-memory data store, meaning it reads and writes data extremely quickly compared to disk-based databases. This makes it ideal for caching purposes where speed is critical.
 
 ## High-level Architecture diagram
![image](https://github.com/user-attachments/assets/63d3b23a-860a-4352-b1b6-ef494750bc52)

- **Logging and Monitoring**:
   - **Prometheus**: Used for collecting and monitoring metrics.
   - **Grafana**: Provides visualization and dashboarding of monitoring data.
   - **ELK Stack (Elasticsearch, Logstash, Kibana)**: For centralized logging and log analysis.

 ## Data flow diagram
 ![image](https://github.com/user-attachments/assets/a40b5b73-d118-4638-ad16-1c97a550209e)

- **Preparation**
   - 0. Admin: Create some quiz
- **Start a Quiz Session**
   - 1.1. User A: Create a Quiz Session (get a unique Session ID).
   - 2.1. User B: Join a Quiz Session.
   - 2.2. System: Raise an event UserJoined.
   - 2.3. User A: Start a Quiz Session.
   - 2.4. System: Prepare a list of questions and answers for a Quiz Session.
   - 2.5. System: Raise event SessionStarted.
   - 2.6. System: Subscribe to event SessionStarted.
   - 2.7. UI: Change to Quiz Challenging screen.
- **Update user score**
   - 3.1. User: Submit an answer.
   - 3.2. System: Raise event UserSubmitted.
   - 3.3. System: Subscribe to event UserSubmitted.
   - 3.4. System: Calculate user score and send an update command.
- **Update leaderboard**
   - 4.1. System: Consume event UserSubmitted.
   - 4.2. System: Calculate new ranking and raise event RankingUpdated.
   - 4.3. System: Consume event RankingUpdated.
   - 4.4. System: Send a command to update leaderboard.
