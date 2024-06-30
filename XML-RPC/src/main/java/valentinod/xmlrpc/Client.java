package valentinod.xmlrpc;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.StandardCharsets;

public class Client {
    public static String fetchXML(String urlString) throws Exception {
        URL url = new URL(urlString);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();

        connection.setRequestMethod("POST");
        connection.setDoOutput(true);

        connection.setRequestProperty("Content-Type", "application/xml");
        connection.setRequestProperty("Accept", "application/xml");

        String xmlData = "<methodCall>\n" +
                "        <methodName>handler.getTemperature</methodName>\n" +
                "        <params>\n" +
                "            <param><value><string>Opatija</string></value></param>\n" +
                "        </params>\n" +
                "    </methodCall>";

        try (OutputStream os = connection.getOutputStream()) {
            byte[] input = xmlData.getBytes(StandardCharsets.UTF_8);
            os.write(input, 0, input.length);
            os.flush();
        }

        BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        String inputLine;
        StringBuilder content = new StringBuilder();

        while ((inputLine = in.readLine()) != null) {
            content.append(inputLine);
        }

        in.close();
        connection.disconnect();

        return content.toString();
    }

    public static void main(String[] args) throws MalformedURLException, XmlRpcException {
        try {
            String xmlContent = fetchXML("http://localhost:8000/xmlrpc/");
            System.out.println(xmlContent);
        } catch (Exception e) {
            e.printStackTrace();
        }

        XmlRpcClientConfigImpl cf = new XmlRpcClientConfigImpl();
        cf.setServerURL(new URL("http://localhost:8000/xmlrpc/"));
        cf.setConnectionTimeout(60000);
        XmlRpcClient client = new XmlRpcClient();
        client.setConfig(cf);
        Object[] params = new Object[] {"Rijeka"};
        Double s = (Double) client.execute("handler.getTemperature", params);
        System.out.println(s);

        params = new Object[] {5, 5};
        Integer i = (Integer) client.execute("handler.add", params);
        System.out.println(i);
    }
}
