package valentinod.xmlrpc;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.webserver.WebServer;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;


import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Main {

    public double getTemperature(String city) {
        try {
            URL url = new URL("https://vrijeme.hr/hrvatska_n.xml");
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");

            BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            String inputLine;
            StringBuilder content = new StringBuilder();

            while ((inputLine = in.readLine()) != null) {
                content.append(inputLine);
            }

            in.close();
            connection.disconnect();

            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            InputSource is = new InputSource(new StringReader(content.toString()));
            Document doc = builder.parse(is);

            NodeList cities = doc.getElementsByTagName("Grad");
            for (int i = 0; i < cities.getLength(); i++) {
                Node node = cities.item(i);
                if (node.getNodeType() != Node.ELEMENT_NODE) continue;

                Element elem = (Element) node;
                String cityName = elem.getElementsByTagName("GradIme").item(0).getTextContent();
                if (cityName.equalsIgnoreCase(city)) {
                    Element data = (Element) elem.getElementsByTagName("Podatci").item(0);
                    Element temp = (Element) data.getElementsByTagName("Temp").item(0);
                    return Double.parseDouble(temp.getTextContent());
                }
            }

            return -273.15; // not found
        } catch (Exception ex) {
            ex.printStackTrace();
            return -273.15;
        }
    }

    public int add(int a, int b) {
        return a + b;
    }

    public static void main(String[] args) throws XmlRpcException, IOException {
        System.out.println("starting...");
        WebServer webServer = new WebServer(8000);

        XmlRpcServer xmlRpcServer = webServer.getXmlRpcServer();

        PropertyHandlerMapping phm = new PropertyHandlerMapping();

        phm.addHandler("handler", Main.class);
        xmlRpcServer.setHandlerMapping(phm);

        webServer.start();
        System.out.println("running");
    }
}
