package iot.house.automation.microservices.mobile.application.interfaces;

import java.util.List;

public interface NoSqlDatabase<TDocument, TDocumentID> {
    void save(TDocument document);
    TDocument find(TDocumentID id);
    List<TDocument> findAll();
    void delete(TDocumentID id);
}
