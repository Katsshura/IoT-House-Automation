package iot.house.automation.microservices.mobile.domain.models.arduino;

import iot.house.automation.microservices.mobile.domain.models.arduino.events.Event;

import java.net.InetAddress;
import java.util.List;
import java.util.UUID;

public record Arduino(UUID uniqueIdentifier, String name, InetAddress IP, int port, List<Event> events) { }