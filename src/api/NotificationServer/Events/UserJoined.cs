﻿namespace NotificationServer.Events;

public record UserJoined(long QuizSessionId, long ParticipantId, string UserName) : IQuizSessionMessage;